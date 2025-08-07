import React from "react";
import { motion } from "framer-motion";

const letterAnimation = {
  hidden: { y: 20, opacity: 0 },
  visible: {
    y: 0,
    opacity: 1,
    transition: { type: "spring", damping: 12, stiffness: 200 },
  },
};

const containerAnimation = {
  hidden: {},
  visible: {
    transition: { staggerChildren: 0.03 },
  },
};

// textParts: mảng [{ text: "Chúng tôi ", className: "" }, { text: "nỗ lực", className: "text-primary" }]
export const BouncingText = ({ textParts = [], wrapperClass = "" }) => {
  return (
    <motion.div
      className={wrapperClass}
      variants={containerAnimation}
      initial="hidden"
      animate="visible"
      style={{ display: "inline-block", flexWrap: "wrap" }}
    >
      {textParts.map((part, i) =>
        [...part.text].map((char, j) => (
          <motion.span
            key={`${i}-${j}`}
            variants={letterAnimation}
            className={part.className}
            style={{ display: "inline-block" }}
          >
            {char === " " ? "\u00A0" : char}
          </motion.span>
        ))
      )}
    </motion.div>
  );
};
