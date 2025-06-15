import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Header from './components/Header.jsx';
import Footer from './components/Footer.jsx';
import Home from './pages/Home.jsx';

import Services from './pages/Services.jsx';
import LegalServices from './pages/LegalServices.jsx';
import CivilServices from './pages/CivilServices.jsx';
import CivilDuration from'./pages/CivilDuration.jsx';
import LegalDuration from './pages/LegalDuration.jsx';
import BookAppointment from './pages/BookAppointment.jsx';

import Login from './pages/Login.jsx';
import Instruction from './pages/Instructions.jsx';
import InstructionInforPage from './pages/InstructionPages/InstructionInforData.jsx';
import Booking from './pages/Booking.jsx';

function App() {
  return (
    <>
    <Header />
    <div style={{paddingTop: '25px'}}>
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/services" element={<Services />} />
      <Route path="/Instruction" element={<Instruction />} />
      <Route path="/legal-services" element={<LegalServices />} />
      <Route path="/civil-services" element={<CivilServices />} />
      <Route path="/legal-duration" element={<LegalDuration/>} />
      <Route path="/civil-duration" element={<CivilDuration/>} />
      <Route path="/book-appointment" element={<BookAppointment />} />
      <Route path="/login" element={<Login />} />
      <Route path="/booking" element={<Booking/>} />

      {/* Các route từ InstructionInforPage */}
      <Route path="/payment-instruction" element={<InstructionInforPage type="payment" />} />
      <Route path="/dna-testing-procedure" element={<InstructionInforPage type="testing" />} />
      <Route path="/sample-collection-guide" element={<InstructionInforPage type="sample" />} />
      <Route path="/prenatal-dna-testing" element={<InstructionInforPage type="prenatal" />} />
      <Route path="/dna-identification-remains" element={<InstructionInforPage type="remains" />} />
      <Route path="/immigration-sponsorship-dna" element={<InstructionInforPage type="immigration" />} />      
    </Routes>
    </div>
    <Footer />
    </>
  );
}

export default App;

