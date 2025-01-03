import React from 'react';

const NotFound = () => {
    return (
        <div className="container-fluid full-height bg-purple color-yellow d-flex justify-content-center align-items-center text-center">
            <div>
                <h1>404 - Page Not Found</h1>
                <br />
                <p className='display-5'>The page you are looking for does not exist.</p>
            </div>
        </div>
    );
};

export default NotFound;