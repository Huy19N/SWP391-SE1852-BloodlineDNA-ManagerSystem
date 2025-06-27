import React, { useState } from "react";
import api from '../../config/axios.js';
import '../../css/login.css';

function Account(){
    const [isLoading, setIsLoading] = useState(false);
    const [dataUser, setDataUser] = useState([]);

    


    return(
        <div className="d-flex justify-content-center align-items-center min-vh-100" style={{background: 'linear-gradient(90deg,#e2e2e2, #c9d6ff)'}}>
            <div className="authinfor-container d-flex" id="container">
                {/* Left side */}
                <div className="d-flex flex-column justify-content-center align-items-center bg-primary text-white p-4" style={{ width: '35%' }}>
                    <h4>Logo</h4>
                </div>
                {/* Right side */}
                <div style={{ width: '65%' }}>
                    <div className="p-5" style={{ backgroundColor: 'white' }}>
                        <form>
                            <div className="row">
                            <div className="col-md-6 mb-3">
                                <label className="form-label">Your Name</label>
                                <input type="text" className="form-control" placeholder="Harper Kim" />
                            </div>
                            <div className="col-md-6 mb-3">
                                <label className="form-label">User Name</label>
                                <input type="text" className="form-control" placeholder="HarperKimsWorld" />
                            </div>

                            <div className="col-md-6 mb-3">
                                <label className="form-label">Email</label>
                                <input type="email" className="form-control" placeholder="harper_kim@example.com" />
                            </div>
                            <div className="col-md-6 mb-3">
                                <label className="form-label">Password</label>
                                <input type="password" className="form-control" placeholder="*********" />
                            </div>

                            <div className="col-md-6 mb-3">
                                <label className="form-label">Date of Birth</label>
                                <input type="text" className="form-control" placeholder="January 1, 1987" />
                            </div>
                            <div className="col-md-6 mb-3">
                                <label className="form-label">Present Address</label>
                                <input type="text" className="form-control" placeholder="Los Angeles, California, USA" />
                            </div>

                            <div className="col-md-6 mb-3">
                                <label className="form-label">Permanent Address</label>
                                <input type="text" className="form-control" placeholder="1234 Main Street, Los Angeles, CA 90001" />
                            </div>
                            <div className="col-md-6 mb-3">
                                <label className="form-label">City</label>
                                <input type="text" className="form-control" placeholder="Los Angeles" />
                            </div>

                            <div className="col-md-6 mb-3">
                                <label className="form-label">Postal Code</label>
                                <input type="text" className="form-control" placeholder="90012" />
                            </div>
                            <div className="col-md-6 mb-3">
                                <label className="form-label">Country</label>
                                <input type="text" className="form-control" placeholder="USA" />
                            </div>
                            </div>

                            <div className="d-flex justify-content-end">
                            <button type="submit" className="btn btn-primary mt-3 px-4">Save</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Account;