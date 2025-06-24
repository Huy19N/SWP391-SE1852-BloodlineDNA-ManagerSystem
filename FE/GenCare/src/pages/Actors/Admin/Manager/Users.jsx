import React, {useEffect} from "react";
import api from "../../../../config/axios.js";
import { useState } from 'react';
import { toast } from 'react-toastify';
import { useNavigate } from "react-router-dom";
import { Button } from "bootstrap";


function Users(){
    const [isLoading, setIsLoading] = useState(false);
    const [dataUsers, setDataUsers] = useState([]);
    const [dataRoles, setDataRoles] = useState([]);
    const navigate = useNavigate();
    
    //API 
    const fetchDataUser = async (e) => {
    setIsLoading(true);

        try{
            const [userRes,roleRes] = await Promise.all([
                api.get('Users/GetAll'),
                api.get('Roles/GetAll'),
            ]);
            
            const userData = userRes.data.data;
            const roleData = roleRes.data.data;

            const roleMap = {};
            roleData.forEach((role) => {
                roleMap[role.roleId] = role.roleName;
            });
            console.log(userData);
            console.log(roleData);
            setDataUsers(userData);
            setDataRoles(roleMap);

        }
        catch(error){
            console.error("Load error:",error);
            toast.error(error.response.data);
        }
        finally{
            setIsLoading(false);
        }
    }
    
    useEffect(() => {
            fetchDataUser();
        },[]);


    return(
        <div className="container mt-5">
        <h3 className="mb-4">User List</h3>
        <table className="table table-bordered table-striped table-hover">
            <thead className="table-light">
            <tr>
                <th>ID</th>
                <th>Email</th>
                <th>Role</th>
            </tr>
            </thead>
            <tbody>
            {isLoading ? (
                <tr>
                <td colSpan="3" className="text-center">Loading...</td>
                </tr>
            ) : dataUsers.length > 0 ? (
                dataUsers.map((dataUsers, index) => (
                <tr key={dataUsers.userId}>
                    <td>{dataUsers.userId}</td>
                    <td>{dataUsers.email}</td>
                    <td>{dataRoles[dataUsers.roleId]}</td>
                    <td><button className="bordernone">Check</button></td>
                    <td><button>Delete</button></td>
                </tr>
                ))
            ) : (
                <tr>
                <td colSpan="3" className="text-center">No users found.</td>
                </tr>
            )}
            </tbody>
        </table>
        </div>
        
    );
}

export default Users;
