import Sidebar from "../../components/Sidebar.jsx";
import { Outlet } from "react-router-dom";

function Layout (){
    return(
        <div className='row g-0 bg-light min-vh-100 margin_top'>
            <aside className="col-md-3 col-lg-2 d-md-block bg-light sidebar border-end fixed-left text-center body_color_1">
                <Sidebar/>
            </aside>
            <main className="col-md-1 ms-sm-auto col-lg-10 px-md-4">
                <Outlet/>
            </main>
        </div>
    );
}

export default Layout;