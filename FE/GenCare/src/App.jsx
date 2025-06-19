import { BrowserRouter, Routes, Route, useLocation } from 'react-router-dom';
import 'react-toastify/dist/ReactToastify.css';
import Header from './components/Header.jsx';
import Footer from './components/Footer.jsx';
import Home from './pages/Home.jsx';
import Zalo from './components/Zalo.jsx';
import ProtectedRoute from './pages/ProtectReute/protectedRoute.jsx';

import Services from './pages/Services.jsx';
import LegalServices from './pages/LegalServices.jsx';
import CivilServices from './pages/CivilServices.jsx';
import CivilDuration from'./pages/CivilDuration.jsx';
import LegalDuration from './pages/LegalDuration.jsx';

import BookAppointment from './pages/BookAppointment.jsx';
import Booking from './pages/Booking.jsx';

import Dashboard from './pages/Actors/Dashboard.jsx';
// import Approve from './pages/Actors/Staff/ApproveForm.jsx';

import Login from './pages/Login.jsx';
import Instruction from './pages/Instructions.jsx';
import InstructionInforPage from './pages/InstructionPages/InstructionInforData.jsx';
// import { use } from 'react';

function App() {
  // Lấy đường dẫn hiện tại
  const location = useLocation();
  // Kiểm tra xem đường dẫn có phải là một trong những đường dẫn không cần hiển thị Header và Zalo hay không
  // Danh sách các đường dẫn không cần hiển thị Header và Zalo
  const anonymousPaths = ['/dashboard', '/approve'];
  // Kiểm tra xem đường dẫn hiện tại có nằm trong danh sách không
  // Nếu đường dẫn hiện tại không nằm trong danh sách, thì hiển thị Header và Zalo
  // Nếu trong localtion.pathname có nằm trong anonymousPaths thì sẽ không hiển thị Header và Zalo
  const isAnonymous = anonymousPaths.includes(location.pathname);
  return (
    <>
    {!isAnonymous && <Header />}
    {!isAnonymous && <Zalo />}
    <div style={{paddingTop: '25px'}}>
    <Routes>
      {/*trang Home*/}
      <Route path="/" element={<Home />} />

      <Route path="/services" element={<Services/>}/>
      <Route path="/Instruction" element={<Instruction />} />
      <Route path="/legal-services" element={<LegalServices />} />
      <Route path="/civil-services" element={<CivilServices />} />

      <Route path="/legal-duration" element={
        <ProtectedRoute allowedRoles={[1, 2, 3, 4]}>
          <LegalDuration />
        </ProtectedRoute>
      } />

      <Route path="/civil-duration" element={
        <ProtectedRoute allowedRoles={[1, 2, 3, 4]}>
          <CivilDuration />
        </ProtectedRoute>
      } />

      <Route path="/book-appointment" element={
        <ProtectedRoute allowedRoles={[1, 2, 3, 4]}>
          <BookAppointment />
        </ProtectedRoute>
      } />

      <Route path="/booking" element={
        <ProtectedRoute allowedRoles={[1, 2, 3, 4]}>
          <Booking />
        </ProtectedRoute>
      } />

      <Route path="/login" element={<Login />} />
      
      <Route path="/dashboard" element={
        <ProtectedRoute allowedRoles={[2, 3, 4]}>
          <Dashboard />
        </ProtectedRoute>
      } />
      {/* <Route path="/approve" element={<Approve />} /> */}

      {/* Các route từ InstructionInforPage */}
      <Route path=" " element={<InstructionInforPage type="payment" />} />
      <Route path="/dna-testing-procedure" element={<InstructionInforPage type="testing" />} />
      <Route path="/sample-collection-guide" element={<InstructionInforPage type="sample" />} />
      <Route path="/prenatal-dna-testing" element={<InstructionInforPage type="prenatal" />} />
      <Route path="/dna-identification-remains" element={<InstructionInforPage type="remains" />} />
      <Route path="/immigration-sponsorship-dna" element={<InstructionInforPage type="immigration" />} />      
    </Routes>
    </div>
    {!isAnonymous && <Footer />}
    </>
  );
}

export default App;

