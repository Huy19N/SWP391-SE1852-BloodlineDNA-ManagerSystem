import React from "react";



function HandleBookingSubmit(){

}

function LoadMethod(){
    // const data = LoadMethod();
    fetch("https://jsonplaceholder.typicode.com/users")

}

function Booking(){
    return(
        <form onSubmit={HandleBookingSubmit}>
            <div className="form-group">
                <label htmlFor="name">Name</label>
                <input type="text" placeholder="Enter your name" />
            </div>
            
            <input type="text" placeholder=""/>
        </form>
    );
}
export default Booking;

