// PieChart.jsx
import React from 'react';
import { Pie } from 'react-chartjs-2';
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';

ChartJS.register(ArcElement, Tooltip, Legend);

function PieChart({ labels, data }) {
  const chartData = {
    labels: labels,
    datasets: [
      {
        label: 'User Distribution',
        data: data,
        backgroundColor: ['#4a90e2', '#ff6384'], // Admin, Customer
        borderWidth: 1,
      },
    ],
  };

  const options = {
    responsive: true,
    plugins: {
      legend: {
        position: 'bottom',
      },
    },
  };

  return <Pie data={chartData} options={options} />;
}

export default PieChart;
