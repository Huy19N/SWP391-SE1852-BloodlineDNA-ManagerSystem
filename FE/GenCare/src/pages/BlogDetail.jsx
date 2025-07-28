import React, { useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";
import api from "../config/axios.js";

function BlogDetail() {
  const { id } = useParams();
  const [blog, setBlog] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchBlogDetail = async () => {
      setLoading(true);
      setError(null);
      try {
        const res = await api.get(`/Blogs/GetById/${id}`);
        setBlog(res.data.data);
      } catch (err) {
        setError("Không thể tải chi tiết bài viết.");
      } finally {
        setLoading(false);
      }
    };
    fetchBlogDetail();
  }, [id]);

  if (loading) {
    return (
      <div className="container py-5 text-center">
        <div className="spinner-border text-primary" role="status" />
        <p>Đang tải chi tiết bài viết...</p>
      </div>
    );
  }

  if (error) {
    return (
      <div className="container py-5 text-center">
        <p className="text-danger">{error}</p>
        <Link to="/blog" className="btn btn-secondary mt-3">Quay lại Blog</Link>
      </div>
    );
  }

  if (!blog) {
    return null;
  }

  return (
    <div className="container py-5 d-flex justify-content-center align-items-center" style={{ minHeight: '60vh' }}>
      <div className="card shadow-lg w-100" style={{ maxWidth: '700px' }}>
        <div className="card-body text-center">
          <h2 className="card-title text-primary mb-3">{blog.title}</h2>
          <p className="card-text text-dark" style={{ fontSize: '1.1rem' }}>{blog.content}</p>
          <Link to="/blog" className="btn btn-outline-primary mt-4">Quay lại Blog</Link>
        </div>
      </div>
    </div>
  );
}

export default BlogDetail;
