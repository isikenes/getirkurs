import './App.css';
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/js/bootstrap.bundle.min";
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Login from './pages/Login';
import Register from './pages/Register';
import ProtectedRoute from './components/ProtectedRoute';
import Home from './pages/Home';
import Navbar from './components/Navbar';
import Footer from './components/Footer';
import CourseDetails from './pages/CourseDetails';

function App() {
  return (
    <Router>
      <Navbar />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/home" element={<Home />} />
        <Route path='/courses/:id' element={<CourseDetails />} />
        
        <Route path="/dashboard" element={
          <ProtectedRoute>
            <Home />
          </ProtectedRoute>
        }
        />
      </Routes>
      <Footer />
    </Router>
  );
}

export default App;
