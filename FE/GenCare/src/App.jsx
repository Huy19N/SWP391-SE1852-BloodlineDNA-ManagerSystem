import { BrowserRouter, Routes, Route, useLocation } from 'react-router-dom';
import 'react-toastify/dist/ReactToastify.css';
import Header from './components/Header.jsx';
import Footer from './components/Footer.jsx';
import Home from './pages/Home.jsx';
import Zalo from './components/Zalo.jsx';
import ChatBot from './components/Chatbot.jsx';
import ProtectedRoute from './pages/ProtectReute/protectedRoute.jsx';

import Services from './pages/Services.jsx';
import LegalServices from './pages/LegalServices.jsx';
import CivilServices from './pages/CivilServices.jsx';
import CollectionMethod from './pages/CollectionMethod.jsx';
import Duration from './pages/Duration.jsx';
import Blog from './pages/Blog.jsx';

import BookAppointment from './pages/BookAppointment.jsx';
import Booking from './pages/Booking.jsx';
import Payment from './pages/Payment/Payment.jsx';
import PaymentSuccess from './pages/Payment/PaymentSuccess.jsx';
import Account from './pages/Actors/InforAccount.jsx';
import MyBooking from './pages/MyBooking.jsx';

import Dashboard from './pages/Actors/Dashboard.jsx';
import Approve from './pages/Actors/Staff/ApproveForm.jsx';
import Users from './pages/Actors/Admin/Manager/Users.jsx';
import Feedbacks from './pages/Actors/Feedbacks.jsx';
import Layout from './pages/Actors/TotalLayout.jsx';
import DBBooking from './pages/Actors/CBooking.jsx';
import CRUDService from './pages/Actors/Admin/Manager/Services.jsx';
import CRUDCollectionMethod from './pages/Actors/Admin/Manager/CollectionsMethod.jsx';
import CRUDDurations from './pages/Actors/Admin/Manager/Durations.jsx';
import CRUDSamples from './pages/Actors/Admin/Manager/Samples.jsx';
import CRUDStepTest from './pages/Actors/Admin/Manager/StepTest.jsx';
import CRUDStatus from './pages/Actors/Admin/Manager/Status.jsx';
import CRUDPrice from './pages/Actors/Admin/Manager/ServicesPrice.jsx';
import CRUDResults from './pages/Actors/Results.jsx';
import CRUDBlog from './pages/Actors/Blog.jsx';

import Login from './pages/Login.jsx';
import Instruction from './pages/Instructions.jsx';
import InstructionInforPage from './pages/InstructionPages/InstructionInforData.jsx';
// import { use } from 'react';

function App() {
  // Lấy đường dẫn hiện tại
  const location = useLocation();
  // Kiểm tra xem đường dẫn có phải là một trong những đường dẫn không cần hiển thị Header và Zalo hay không
  // Danh sách các đường dẫn không cần hiển thị Header và Zalo
  const anonymousPaths = ['/layout','/payment-success'];
  // Kiểm tra xem đường dẫn hiện tại có nằm trong danh sách không
  // Nếu đường dẫn hiện tại không nằm trong danh sách, thì hiển thị Header và Zalo
  // Nếu trong localtion.pathname có nằm trong anonymousPaths thì sẽ không hiển thị Header và Zalo
  // startswith nó sẽ ẩn pathname bắt đầu với route có dữ liệu trong anonymousPaths
  // lúc đầu dùng inclucdes thì nó so sánh tuyệt đối nhưng vẫn có thể dùng nhưng không có ứng biến tốt.
  const isAnonymous = anonymousPaths.some(path => location.pathname.startsWith(path));

  return (
    <>
    {!isAnonymous && <Header />}
    {!isAnonymous && <Zalo />}
    {!isAnonymous && <ChatBot />}
    
    <Routes>
      {/*trang Home*/}
      <Route path="/" element={<Home />} />

      <Route path="/services" element={<Services/>}/>
      <Route path="/Instruction" element={<Instruction />} />
      <Route path="/legal-services" element={<LegalServices />} />
      <Route path="/civil-services" element={<CivilServices />} />
      <Route path="/collection-method" element={<CollectionMethod/>} />
      <Route path="/blog" element={<Blog />} />

      
       <Route path="/duration" element={
        <ProtectedRoute allowedRoles={[1, 2, 3, 4]}>
          <Duration />
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
      
        <Route path='/layout' element={
          <ProtectedRoute allowedRoles={[2, 3, 4]}>
            <Layout />
          </ProtectedRoute>}>
          <Route index element={<Dashboard/>}/>
          <Route path='dashboard' element={<Dashboard/>}/>
          <Route path='approve' element={<Approve/>}/>
          <Route path='users' element={<Users/>}/>
          <Route path='dbbooking' element={<DBBooking/>}/>
          <Route path='services' element={<CRUDService/>}/>
          <Route path='collectionmethod' element={<CRUDCollectionMethod/>}/>
          <Route path='durations' element={<CRUDDurations/>}/>
          <Route path='samples' element={<CRUDSamples/>}/>
          <Route path='steptest' element={<CRUDStepTest/>}/>
          <Route path='status' element={<CRUDStatus/>}/>
          <Route path='price' element={<CRUDPrice/>}/>
          <Route path='results' element={<CRUDResults/>}/>
          <Route path='feedback' element={<Feedbacks/>}/>
          <Route path='blog' element={<CRUDBlog/>}/>
        </Route>

      <Route path='/payment' element={
        <ProtectedRoute allowedRoles={[1, 2, 3, 4]}>
          <Payment/>
        </ProtectedRoute>
      }/>

      <Route path='/mybooking' element={
        <ProtectedRoute allowedRoles={[1, 2, 3, 4]}>
          <MyBooking/>
        </ProtectedRoute>
      }/>

      <Route path='/payment-success' element={<PaymentSuccess/>}/>

      <Route path='/account' element={
        <ProtectedRoute allowedRoles={[1, 2, 3, 4]}>
          <Account/>
        </ProtectedRoute>
      }/>

      {/* Các route từ InstructionInforPage */}
      <Route path="/payment-instruction" element={<InstructionInforPage type="payment" />} />
      <Route path="/dna-testing-procedure" element={<InstructionInforPage type="testing" />} />
      <Route path="/sample-collection-guide" element={<InstructionInforPage type="sample" />} />
      <Route path="/prenatal-dna-testing" element={<InstructionInforPage type="prenatal" />} />
      <Route path="/dna-identification-remains" element={<InstructionInforPage type="remains" />} />
      <Route path="/immigration-sponsorship-dna" element={<InstructionInforPage type="immigration" />} />      
    </Routes>
    
    {!isAnonymous && <Footer />}
    </>
  );
}

export default App;

