import React, { useEffect, useState } from 'react'
import { getAllCourses } from '../services/api';
import CourseCard from '../components/CourseCard';

const Home = () => {
  const [courses, setCourses] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchCourses = async () => {
      try {
        const courses = await getAllCourses();
        setCourses(courses);
      } catch (error) {
        setError(error);
      } finally {
        setLoading(false);
      }
    };
    fetchCourses();
  }, []);

  if (loading) return <p>Yükleniyor...</p>;
  if (error) return <p>{error}</p>;

  return (
    <div className="container full-height">
      <h1 className="my-4">Courses</h1>
      <div className="row">
        {courses.map((course) => (
          <div className="col-12 col-md-6 col-lg-4" key={course.id}>
            <CourseCard course={course} />
          </div>
        ))}
      </div>
    </div>
  );
};

export default Home;