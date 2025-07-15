import axios from 'axios';

const BASE_URL = 'https://localhost:7722/api/Payment';

export const createPayment = async (data) => {
  const res = await axios.post(BASE_URL, data);
  return res.data.data; // URL thanh toán
};
