import ListGroup from 'react-bootstrap/ListGroup';
import '../functionalComponents/CustomStyles.css';
import React, {useState, useEffect} from 'react';
import {useNavigate,useParams} from 'react-router-dom';
import Thread from "./Threads";
import Spinner from 'react-bootstrap/Spinner';
function PostsList(){
    const [postsList,setPostsList] = useState(null);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();
    const params=useParams();
    useEffect(() => {
        fetch(process.env.REACT_APP_API+'topics/'+params.topicsId+'/threads/'+params.threadsId+'/posts',{
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
                }else if(response.status==404){
                    alert("Requested thread does not exist.")
                    navigate('/topics/'+params.topicsId+"/threads");
                }else{
                    alert("Error:"+response.status+"\nMessage:"+response.statusText)
                }
            }
            return response.json();           
        })
        .then(data=>{
            setPostsList(data);
            setLoading(false);
            console.log(data);
        });
    }, [])
    function NavigateToPage(int){
        navigate(''+int);
    }

    return(
        
        <div className="container mt-5" >
            {loading ? (
            <div style={{color:'rgb(255,255,255)'}}>
            <Spinner animation="border" role="status">
                <span className="visually-hidden">Loading...</span></Spinner>
            A moment please... 
            </div>            
            ) : (
            <div>
            <Thread/>
         
            <ListGroup className='col-md-10 offset-md-1'>
                {postsList.map((element) => 
                    <ListGroup.Item className="d-flex justify-content-between align-items-start p-3" action href={"posts/"+element.id} key={element.id} style={{backgroundColor:'rgb(76, 53, 117)'}}>
                        <div className="description" >
                            {element.description}
                        </div>
                    </ListGroup.Item>
                )}
            </ListGroup>
            </div>
            )}
        </div>
    )
}
export default PostsList;