import React, { useState } from "react";
import api from '../../config/axios.js';
import '../../css/login.css';

function Account(){
    const [isLoading, setIsLoading] = useState(false);



    return(
        <div className="d-flex justify-content-center align-items-center min-vh-100" style={{background: 'linear-gradient(90deg,#e2e2e2, #c9d6ff)'}}>
            <div className="authinfor-container" id="container">
                <div className="text-center mt-3 pt-2"><h1 className="border-bottom text-primary">Welcome Account</h1></div>
            </div>
            
        
        </div>
    );
}

export default Account;