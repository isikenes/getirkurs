import React from 'react';
import { MoonLoader } from 'react-spinners';

const LoadingSpinner = ({ loading }) => {
    return (
        <div className="d-flex justify-content-center align-items-center full-height">
            <MoonLoader color="#6541ce" loading={loading} size={50} />
        </div>
    );
};

export default LoadingSpinner;