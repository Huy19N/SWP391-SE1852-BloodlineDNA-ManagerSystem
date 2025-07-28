import logo1 from "../assets/logo1.png";
import img1 from '../assets/ServicesHome.jpg';
import img2 from '../assets/staff.jpg';
import img3 from '../assets/test1.jpg';
import img4 from '../assets/test2.jpg';
import React from "react";
import { Link } from "react-router-dom";

import { useEffect, useState } from "react";
import api from "../config/axios.js";



// Blog Page Component
function Blog() {
    
    const [blogs, setBlogs] = useState([]);
    const [loading, setLoading] = useState(false);

    //Gọi API
    const fetchBlogs = async (e) => {
        setLoading(true);

        try{
            const resBlogs = await api.get('Blogs/GetAllPaging');
            setBlogs(resBlogs.data.data);

            console.log(resBlogs.data.data);
        }
        catch (error) {
            console.error("Error fetching blogs:", error.resBlogs.data.data);
        } finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        fetchBlogs();
    }, []);

  return (
    <div>
      <div className="container-fluid p-0 m-0 position-relative">
        <div id="demo" className="carousel slide" data-bs-ride="carousel">
          {/* Indicators */}
          <div className="carousel-indicators">
            <button type="button" data-bs-target="#demo" data-bs-slide-to="0" className="active"></button>
            <button type="button" data-bs-target="#demo" data-bs-slide-to="1"></button>
            <button type="button" data-bs-target="#demo" data-bs-slide-to="2"></button>
            <button type="button" data-bs-target="#demo" data-bs-slide-to="3"></button>
          </div>

          {/* Carousel images */}
          <div className="carousel-inner">
            <div className="carousel-item active position-relative w-100" style={{ aspectRatio: "16/6" }}>
              <img src={img1} alt="Slide 1" className="d-block position-absolute top-0 start-0 w-100 h-100 object-fit-cover" />
            </div>
            <div className="carousel-item position-relative w-100" style={{ aspectRatio: "16/6" }}>
              <img src={img2} alt="Slide 2" className="d-block position-absolute top-0 start-0 w-100 h-100 object-fit-cover" />
            </div>
            <div className="carousel-item position-relative w-100" style={{ aspectRatio: "16/6" }}>
              <img src={img3} alt="Slide 3" className="d-block position-absolute top-0 start-0 w-100 h-100 object-fit-cover" />
            </div>
            <div className="carousel-item position-relative w-100" style={{ aspectRatio: "16/6" }}>
              <img src={img4} alt="Slide 4" className="d-block position-absolute top-0 start-0 w-100 h-100 object-fit-cover" />
            </div>
          </div>

          {/* Controls */}
          <button className="carousel-control-prev" type="button" data-bs-target="#demo" data-bs-slide="prev">
            <span className="carousel-control-prev-icon"></span>
          </button>
          <button className="carousel-control-next" type="button" data-bs-target="#demo" data-bs-slide="next">
            <span className="carousel-control-next-icon"></span>
          </button>
        </div>

        {/* Text overlay */}
        <div className="position-absolute top-50 start-50 translate-middle text-center text-white">
          <h1 className="fw-bold display-4 text-primary">Blog</h1>
        </div>
      </div>

    {/*Blog */}
      <div className="container py-5">
        {loading ? (
          <div className="text-center">
            <div className="spinner-border text-primary" role="status" />
            <p>Đang tải bài viết...</p>
          </div>
        ) : (
          <div className="row g-4">
            {blogs.map((blog, index) => (
              <div key={index} className="col-12 col-md-4 d-flex">
                <div className="bg-white shadow-lg rounded-lg overflow-hidden h-100 d-flex flex-column">
                  <div className="p-4 flex-grow-1">
                    <h5 className="text-xl font-bold mb-2">{blog.title}</h5>
                    <p className="text-sm text-gray-700 mb-3">
                      {blog.content?.substring(0, 75) + "..."}
                    </p>
                      <Link
                        to={`/blog/${blog.blogId}`}
                        className="btn btn-primary mt-auto"
                      >
                        Read More
                      </Link>
                  </div>
                </div>
              </div>
            ))}
          </div>
        )}
      </div>
    </div>
  );
}
export default Blog;
