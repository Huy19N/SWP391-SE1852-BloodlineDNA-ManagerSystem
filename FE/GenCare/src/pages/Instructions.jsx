import Card from "../components/InstructionCard";
import CardData from "../components/InstructionCardData";
import "../css/InstructionCard.css"

function Instruction(){
    const card = CardData.map(item => {
        return <Card image ={item.img} name={item.name} des={item.des}/>
    })
    return(
        <>  
            <h1 className="heading">Hướng dẫn</h1>
            <div className="header_underline"></div>
            <div className="wrapper">{card}</div>
        </>
    );
}

export default Instruction