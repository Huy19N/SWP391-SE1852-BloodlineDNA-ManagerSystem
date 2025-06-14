function InstructionCard(props){
    return(
      <div className="card">
        <img src={props.image}/>
        <h3>{props.name}</h3>
        <p>{props.des}</p>
        <a href="#" className="bts">
            Chi tiết
        </a>
      </div>  
    );
}

export default InstructionCard