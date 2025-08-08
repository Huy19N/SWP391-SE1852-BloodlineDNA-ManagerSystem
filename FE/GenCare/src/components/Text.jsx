import { motion } from "framer-motion";
import "../css/index.css";

const ScaleInText = ({
  text = "Think Different",
  className = ""
}) => {
  return (
    <h2
      className={`relative text-4xl md:text-6xl font-extrabold text-center tracking-wider leading-snug 
        bg-gradient-to-r from-sky-400 via-blue-500 to-cyan-400 text-uppercase
        bg-clip-text text-primary text-transparent drop-shadow-[2px_2px_4px_rgba(0,0,0,0.25)]
        ${className}`}
    >
      {text.split("").map((char, i) => (
        <motion.span
          key={i}
          initial={{
            scale: 0.5,
            opacity: 0,
            rotate: -10
          }}
          animate={{
            scale: 1,
            opacity: 1,
            rotate: 0
          }}
          transition={{
            delay: i * 0.05,
            type: "spring",
            stiffness: 180,
            damping: 12
          }}
          className="inline-block shine-effect"
        >
          {char === " " ? "\u00A0" : char}
        </motion.span>
      ))}
    </h2>
  );
};

const ScaleInView = () => {
  return (
    <div className="flex flex-col items-center justify-center font-sans mt-5 mb-5 p-4 gap-8">
      <ScaleInText text="Chúng tôi nỗ lực bạn hài lòng" />
      <ScaleInText text="Chúng tôi chính xác bạn hi vọng" />
    </div>
  );
};

export default ScaleInView;
