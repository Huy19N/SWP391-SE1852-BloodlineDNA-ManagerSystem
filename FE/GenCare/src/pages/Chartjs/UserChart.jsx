import React, { useEffect, useState } from 'react';
import { Pie } from 'react-chartjs-2';
import {
  Chart as ChartJS,
  ArcElement,
  Tooltip,
  Legend
} from 'chart.js';
import api from '../../config/axios.js';

ChartJS.register(ArcElement, Tooltip, Legend);

    function UserRolePieChart() {
        const [chartData, setChartData] = useState(null);

        // Cấu hình mapping tên Role
        const roleMap = {
            1: "Customer",
            2: "Staff",
            3: "Manager",
            4: "Admin"
        };

        useEffect(() => {
            const fetchUsers = async () => {
            try {
                const res = await api.get('/Users/GetAll');
                const users = res.data.data;

                // Đếm số user theo roleId
                const roleCounts = {};
                users.forEach(user => {
                const roleId = user.roleId;
                roleCounts[roleId] = (roleCounts[roleId] || 0) + 1;
                });

                const labels = Object.keys(roleCounts).map(roleId => roleMap[roleId] || `Role ${roleId}`);
                const data = Object.values(roleCounts);

                setChartData({
                labels,
                datasets: [{
                    label: 'User Count by Role',
                    data,
                    backgroundColor: [
                    'rgba(255, 99, 132, 0.6)',   
                    'rgba(54, 162, 235, 0.6)',   
                    'rgba(255, 205, 86, 0.6)',
                    'rgba(75, 192, 192, 0.6)'     
                    ],
                    hoverOffset: 6
                }]
                });
            } catch (err) {
                console.error('Error fetching user data:', err);
            }
            };

            fetchUsers();
        }, []);

        return (
            <div className="ms-4 mt-5 pt-4 bg-white rounded shadow">
            <h5 className="mb-3 text-center text-primary">USER REPORT</h5>
            {chartData ? (
                <Pie data={chartData} />
            ) : (
                <p className='text-center text-danger'>Loading chart...</p>
            )}
            </div>
        );
}

export default UserRolePieChart;
