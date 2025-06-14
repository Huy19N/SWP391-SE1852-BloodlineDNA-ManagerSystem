import Card from "../components/Card";
import CardData from "../components/CardData";
import "../css/Card.css"

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