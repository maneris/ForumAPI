import ListGroup from 'react-bootstrap/ListGroup';
import '../functionalComponents/CustomStyles.css';
import React, {useState, useEffect} from 'react';
import {useNavigate} from 'react-router-dom';
import Spinner from 'react-bootstrap/Spinner';
import TopicCreate from './Modals/TopicCreate';

function TopicsList(){
    const [topicsList,setTopicsList] = useState(null);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();
    useEffect(() => {
        fetch(process.env.REACT_APP_API+'topics',{
            method:'get',
            mode: 'cors',
            headers:{
                'Authorization':"Bearer " + sessionStorage.getItem("token")
            },
        })
        .then((response) => {
            if (!response.ok) {
                if(response.status==401){
                    sessionStorage.clear();
                    alert("Session expired please relogin.")
                    navigate('/login');
                }else{
                    alert("Error:"+response.status+"\nMessage:"+response.statusText)
                }
            }
            return response.json();           
        })
        .then(data=>{
            setTopicsList(data);
            setLoading(false);
            console.log(data);
        });
    }, [])
    function NavigateToPage(int){
        navigate(''+int);
    }

    return(
        <div className="container col-md-10 offset-md-1 mt-5" >
            <div className='offset-md-5'><TopicCreate /></div>
            {loading ? (
                <div style={{color:'rgb(255,255,255)'}}>
                <Spinner animation="border" role="status">
                    <span className="visually-hidden">Loading...</span></Spinner>
                A moment please... 
                </div>
            ) : (
            <ListGroup className=' col-md-10 offset-md-1' >
                {topicsList.map((element) => 
                    <ListGroup.Item className="d-flex justify-content-between align-items-start" action href={"topics/"+element.id+"/threads"} key={element.id} style={{backgroundColor:'rgb(76, 53, 117)'}}>
                        <div className="ms-2 me-auto description" >
                            <div className="title">{element.title}</div>
                            {element.description}
                        </div>
                    </ListGroup.Item>
                )}
            </ListGroup>
            )}
        </div>
    )
}
export default TopicsList