import React, { useEffect, useState } from 'react'
import { getAllCourses } from '../services/api';
import CourseCard from '../components/CourseCard';
import LoadingSpinner from '../components/LoadingSpinner';

const Home = () => {
  const [courses, setCourses] = useState([]);
  const [filteredCourses, setFilteredCourses] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [searchQuery, setSearchQuery] = useState("");

  useEffect(() => {
    const fetchCourses = async () => {
      try {
        const courses = await getAllCourses();
        setCourses(courses);
        setFilteredCourses(courses);
      } catch (error) {
        setError(error);
      } finally {
        setLoading(false);
      }
    };
    fetchCourses();
  }, []);

  const handleSearch = (event) => {
    const query = event.target.value.toLowerCase();
    setSearchQuery(query);

    const filtered = courses.filter((course) =>
      course.title.toLowerCase().includes(query)
    );
    setFilteredCourses(filtered);
  };

  if (loading) return <LoadingSpinner loading={loading} />;
  if (error) return <p>{error}</p>;

  return (
    <div className="container full-height">
      <div className='row my-4 py-2'>
        <div className='col-md-4'>
          <h1 className="ms-2">Popular Courses</h1>
        </div>

        <div className='col-md-8 d-flex justify-content-center align-items-center'>
          <div className="input-group">
            <input
              type="text"
              className="form-control"
              placeholder="Search for a course..."
              value={searchQuery}
              onChange={handleSearch}
            />
          </div>
        </div>
      </div>

      <div className="row">
        {filteredCourses.map(course => (
          <div key={course.id} className="col-md-4 mb-4">
            <CourseCard course={course} />
          </div>
        ))}
      </div>
    </div>
  );
};

export default Home;