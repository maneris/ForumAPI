import ListGroup from 'react-bootstrap/ListGroup';
import '../functionalComponents/CustomStyles.css';
import React, {useState, useEffect} from 'react';
import {useNavigate} from 'react-router-dom';


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
        <div className="container" >
            {loading ? (
                <div>A moment please...</div>
            ) : (
            <ListGroup>
                {topicsList.map((element) => 
                    <ListGroup.Item className="d-flex justify-content-between align-items-start" action href={"topics/"+element.id} key={element.id} >
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