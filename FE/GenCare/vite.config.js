import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],

  server: {
    host: true,
    port: 5173,
    strictPort: true,
    cors: true,
    allowedHosts: [
      '4e52b0b055e2.ngrok-free.app'
    ]
  },

})
