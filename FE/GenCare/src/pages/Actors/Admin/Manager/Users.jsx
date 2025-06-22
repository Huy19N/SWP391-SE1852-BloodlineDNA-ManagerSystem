import React, {useEffect} from "react";
import api from "../../../../config/axios.js";
import { useState } from 'react';
import { toast } from 'react-toastify';


function Users(){
    const [isLoading, setIsLoading] = useState(false);
    const [dataUsers, setDataUsers] = useState([]);
    const navigate = useNavigate();

    

    return(
        <h1>Welocome Users page</h1>
    );
}

export default Users;
