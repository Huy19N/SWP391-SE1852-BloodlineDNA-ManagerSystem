import Card from "./InstructionPages/InstructionCard";
import CardData from "./InstructionPages/InstructionCardData";
import "../css/InstructionCard.css"

function Instruction(){
    const card = CardData.map(item => {
        return <Card image ={item.img} name={item.name} des={item.des} path={item.path}/>
    })
    return(
        <>  
        <div className="Instruction-page">
            <div className="heading"><h1>Hướng dẫn</h1></div>
            <div className="header_underline"></div>
            <div className="wrapper">{card}</div>
        </div>
        </>
    );
}

export default Instruction