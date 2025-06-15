
const InstructionInfor = ({ title, paragraphs }) => {
  return (
    <div className="info-page">
      <h1 className="heading">{title}</h1>
      <div className="header_underline"></div>
      <div className="content">
        {paragraphs.map((text, index) => (
          <p key={index}>{text}</p>
        ))}
      </div>
    </div>
  );
};

export default InstructionInfor;
