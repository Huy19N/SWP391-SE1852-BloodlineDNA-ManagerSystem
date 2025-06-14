import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Header from './components/Header.jsx';
import Footer from './components/Footer.jsx';
import Home from './pages/Home.jsx';
import Services from './pages/Services.jsx';
import LegalServices from './pages/LegalServices.jsx';
import CivilServices from './pages/CivilServices.jsx';
import Login from './pages/Login.jsx';
import Instruction from './pages/Instructions.jsx';
import Booking from './pages/Booking.jsx';

function App() {
  return (
    <>
    <Header />
    <div style={{paddingTop: '56px'}}>
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/services" element={<Services />} />
      <Route path="/Instruction" element={<Instruction />} />
      <Route path="/legal-services" element={<LegalServices />} />
      <Route path="/civil-services" element={<CivilServices />} />
      <Route path="/login" element={<Login />} />
      <Route path="/booking" element={<Booking/>} />
    </Routes>
    </div>
    <Footer />
    </>
  );
}

export default App;

