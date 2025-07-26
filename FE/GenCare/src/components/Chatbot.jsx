import React, { useState } from 'react';
import img1 from '../assets/blood-drop-svgrepo-com.svg';
import '../css/index.css';

export default function AIChatWidget() {
  const [isOpen, setIsOpen] = useState(false);
  const [messages, setMessages] = useState([]);
  const [input, setInput] = useState('');
  const [loading, setLoading] = useState(false);

  const toggleChat = () => setIsOpen(!isOpen);

  const handleSend = async () => {
    if (!input.trim()) return;

    const userMsg = { role: 'user', content: input };
    setMessages(prev => [...prev, userMsg]);
    setInput('');
    setLoading(true);

    try {
      const res = await fetch('https://openrouter.ai/api/v1/chat/completions', {
        method: 'POST',
        headers: {
          'Authorization': 'Bearer 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',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          model: 'tngtech/deepseek-r1t2-chimera:free',
          messages: [...messages, userMsg].map(m => ({ role: m.role, content: m.content }))
        })
      });
      const data = await res.json();
      const aiReply = data.choices?.[0]?.message?.content || 'Sorry, I could not understand.';
      setMessages(prev => [...prev, { role: 'assistant', content: aiReply }]);
    // eslint-disable-next-line no-unused-vars
    } catch (err) {
      setMessages(prev => [...prev, { role: 'assistant', content: 'Error talking to AI.' }]);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="chat-container">
      {isOpen && (
        <div className="chat-box">
          <div className="chat-header">AI Chat Support</div>
          <div className="chat-messages">
            {messages.map((msg, idx) => (
              <div key={idx} className={`chat-message ${msg.role}`}>
                {msg.content}
              </div>
            ))}
            {loading && <div className="chat-message assistant">Think...</div>}
          </div>
          <div className="chat-input">
            <input
              value={input}
              onChange={e => setInput(e.target.value)}
              onKeyDown={e => e.key === 'Enter' && handleSend()}
              placeholder="Type your message..."
            />
            <button onClick={handleSend}>Send</button>
          </div>
        </div>
      )}

      <div className="ai-chat-button" onClick={toggleChat}>
        <div className="ai-chat-label">
          AI Chat
        </div>
        <div className="ai-chat-icon">
          <img src={img1} alt="Chat" />
        </div>    
      </div>
    </div>
  );
}
