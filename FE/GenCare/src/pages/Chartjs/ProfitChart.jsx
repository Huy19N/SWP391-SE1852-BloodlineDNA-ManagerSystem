import React, { useEffect, useState } from 'react';
import { Bar } from 'react-chartjs-2';
import api from '../../config/axios.js';
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Tooltip,
  Legend,
} from 'chart.js';

ChartJS.register(CategoryScale, LinearScale, BarElement, Tooltip, Legend);

  function Chart() {
    const [chartData, setChartData] = useState({});

    useEffect(() => {
      const fetchPayments = async () => {
        try {
          const res = await api.get('/Payment/GetAll');
          const payments = res.data.data;

          const profitByDate = {};
          payments.forEach(p => {
            const date = p.paymentDate?.split('T')[0];
            if (!profitByDate[date]) profitByDate[date] = 0;
            profitByDate[date] += p.amount;
          });

          const labels = Object.keys(profitByDate).sort();
          const data = labels.map(date => profitByDate[date]);

          setChartData({
            labels,
            datasets: [
              {
                label: 'Profit per Day (VNĐ)',
                data,
                backgroundColor: [

                  'rgba(38, 141, 209, 0.5)'],
                borderColor: [
                  
                  'rgb(54, 162, 235)'],
                borderWidth: 1
              }
            ]
          });
        } catch (error) {
          console.log('Error loading chart data', error);
        }
      };

      fetchPayments();
    }, []);

    return (
      <div className="ms-3 mt-5 p-5 bg-white rounded shadow card" style={{width: '1030px', height: '590px'}}>
        <h5 className="mb-5 text-primary">DAILY PROFIT OVERVIEW</h5>
        {chartData.labels ? (
          <Bar
            data={chartData}
            options={{
              responsive: true,
              scales: {
                y: {
                  beginAtZero: true,
                  ticks: {
                    callback: value => `${value.toLocaleString()} VNĐ`
                  }
                }
              }
            }}
          />
        ) : (
          <p className='text-center text-danger'>Loading chart...</p>
        )}
      </div>
    );
}

export default Chart;
