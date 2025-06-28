import React, { useEffect, useState } from "react";
import api from '../../config/axios.js';
import '../../css/login.css';
import img1 from '../../assets/blood-drop-svgrepo-com.svg';
import { toast } from 'react-toastify';
function Account(){
    const [isLoading, setIsLoading] = useState(false);
    const [dataUser, setDataUser] = useState([]);
    const [dataRole, setDataRole] = useState([]);
    
    const idUser = localStorage.getItem('userId');
    const idRole = localStorage.getItem('roleId');

    const fetchUser = async (e) => {
        setIsLoading(true);

        try{
            const response = await api.get(`Users/getbyid/${idUser}`);
            const resRole = await api.get(`Roles/GetById/${idRole}`);
            const role = resRole.data.data;
            const user = response.data.data;
            setDataUser(user);
            setDataRole(role);

            console.log(user);
        }
        catch(error){
            console.log(error.response.data);
            toast.error("Error data user");
        }
        finally{
            setIsLoading(false);
        }
    } 

    useEffect (() => {
        fetchUser();
    },[]);


    return(
        <div className="d-flex justify-content-center align-items-center min-vh-100" style={{background: 'linear-gradient(90deg,#e2e2e2, #c9d6ff)'}}>
            <div className="authinfor-container d-flex" id="container">
                {/* Left side */}
                <div className="d-flex flex-column justify-content-center align-items-center bg-primary text-white p-4" style={{ width: '35%' }}>
                    <img className="img-fluid img-thumbnail bg-primary border border-0 rounded-circle w-50" src={img1} />
                    <div className="h2 m-4 p-4">
                        Your Account
                    </div>
                </div>
                {/* Right side */}
                <div style={{ width: '65%' }}>
                    <div className="p-5" style={{ backgroundColor: 'white' }}>
                        <form>
                            <div className="row">
                                <div className="col-md-6 mb-3">
                                    <label className="form-label">ID User:</label>
                                    <input type="text"
                                           className="form-control"
                                           value={dataUser.userId}
                                           disabled/>
                                </div>
                                <div className="col-md-6 mb-3">
                                    <label className="form-label">Role</label>
                                    <input type="text" 
                                           className="form-control"
                                            value={dataRole.roleName}
                                            disabled/>
                                </div>

                                <div className="col-md-6 mb-3">
                                    <label className="form-label">Full Name</label>
                                    <input type="text" 
                                           className="form-control"
                                           value={dataUser?.fullName || ''}
                                           disabled/>
                                </div>
                                <div className="col-md-6 mb-3">
                                    <label className="form-label">CMND/CCCD</label>
                                    <input type="text" 
                                           className="form-control"
                                           value={dataUser?.identifyId?.toString() || ''}
                                           disabled/>
                                </div>

                                <div className="col-md-6 mb-3">
                                    <label className="form-label">Address</label>
                                    <input type="text" 
                                           className="form-control"
                                           value={dataUser?.address || ''}
                                           disabled/>
                                </div>
                                <div className="col-md-6 mb-3">
                                    <label className="form-label">Email</label>
                                    <input type="text" 
                                           className="form-control"
                                           value={dataUser?.email || ''}
                                           disabled/>
                                </div>

                                <div className="col-md-6 mb-3">
                                    <label className="form-label">Phone</label>
                                    <input type="text" 
                                           className="form-control"
                                           value={dataUser?.phone?.toString() || ''} 
                                           disabled/>
                                </div>
                                <div className="col-md-6 mb-3">
                                    <label className="form-label">Password</label>
                                    <input type="password" 
                                           className="form-control"
                                           value={dataUser?.password || ''}
                                           disabled/>
                                </div>
                            </div>

                            <div className="d-flex justify-content-end">
                            <button type="submit" className="btn btn-primary mt-3 px-4">Edit</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Account;