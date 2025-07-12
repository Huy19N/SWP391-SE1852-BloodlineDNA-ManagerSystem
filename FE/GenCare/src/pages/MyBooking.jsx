import React, { useEffect, useState } from "react";
import api from '../config/axios.js';
import { ToastContainer, toast } from 'react-toastify';


function MyBooking(){
    const [isLoading, setIsLoading] = useState(false);
    const [search,setSearch] = useState('');

    const roleid = localStorage.getItem('roleId');
    const isStaff = roleid === '2';
    const isAdmin = roleid === '4';
    const isManager = roleid === '3';

    return (
        <div>
            Mybookings
        </div>
    );
}

export default MyBooking;