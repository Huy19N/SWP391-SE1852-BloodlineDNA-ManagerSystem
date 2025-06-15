import { Link } from "react-router-dom";

function InstructionCard(props){
    return(
      <div className="card">
        <img src={props.image}/>
        <h3>{props.name}</h3>
        <p>{props.des}</p>
      <Link to={props.path} className="bts">
        Chi tiết
      </Link>
      </div>  
    );
}

export default InstructionCard