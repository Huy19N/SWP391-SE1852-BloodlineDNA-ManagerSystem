import { BouncingText } from "../components/gsap/bouncing-text";

const Text = () => {
  return (
    <div className="text-center py-5">
      <h2 className="fw-bold display-6">
        <BouncingText
          wrapperClass="text-3xl font-semibold"
          textParts={[
            { text: "Chúng tôi ", className: "" },
            { text: "nỗ lực", className: "text-primary" },
            { text: " bạn ", className: "" },
            { text: "hài lòng", className: "text-primary" },
          ]}
        />
        <br />
        <BouncingText
          wrapperClass="text-3xl font-semibold"
          textParts={[
            { text: "Chúng tôi ", className: "" },
            { text: "chính xác", className: "text-primary" },
            { text: " bạn ", className: "" },
            { text: "hi vọng", className: "text-primary" },
          ]}
        />
      </h2>
    </div>
  );
};

export default Text;
