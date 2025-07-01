import logo1 from "../assets/logo1.png";
import img1 from '../assets/ServicesHome.jpg';
import img2 from '../assets/staff.jpg';
import img3 from '../assets/test1.jpg';
import img4 from '../assets/test2.jpg';

// BlogCard Component
const BlogCard = ({ title, description, image, link }) => {
  return (
    <div className="bg-white shadow-lg rounded-lg overflow-hidden h-100 d-flex flex-column">
      <img src={image} alt={title} className="w-100 h-56 object-cover" />
      <div className="p-4 flex-grow-1">
        <h5 className="text-xl font-bold mb-2">{title}</h5>
        <p className="text-sm text-gray-700 mb-3">{description}</p>
        <a href={link} className="btn btn-primary mt-auto">Read More</a>
      </div>
    </div>
  );
};

// Blog Page Component
function Blog() {
  const blogs = [
    {
      title: "Understanding DNA Testing",
      description: "An overview of how DNA testing works and its applications.",
      image: logo1,
      link: "/blog/dna-testing"
    },
    {
      title: "The Future of Genetic Research",
      description: "Exploring the latest advancements in genetic research.",
      image: logo1,
      link: "/blog/genetic-research"
    },
    {
      title: "Ethical Considerations in DNA Testing",
      description: "Discussing the ethical implications of DNA testing.",
      image: logo1,
      link: "/blog/ethical-dna"
    }
  ];

  return (
    <>
          {/* Carousel Section */}
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

    {/* Blog */}
    <div className="container py-5">
      <div className="row g-4">
        {blogs.map((blog, index) => (
          <div key={index} className="col-12 col-md-4 d-flex">
            <BlogCard {...blog} />
          </div>
        ))}
      </div>
    </div>
    </>
  );
}

export default Blog;
