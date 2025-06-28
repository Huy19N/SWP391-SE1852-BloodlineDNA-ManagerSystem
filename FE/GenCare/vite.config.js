import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],

  server: {
    host: true,              // Cho phép truy cập từ bên ngoài
    port: 5173,
    strictPort: true,
    cors: true,
    allowedHosts: [
      'b4e6-42-117-101-6.ngrok-free.app'  // 👈 Thêm domain ngrok ở đây
    ]
  },
})
