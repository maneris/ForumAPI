import ListGroup from 'react-bootstrap/ListGroup';
import '../functionalComponents/CustomStyles.css';
import React, {useState, useEffect} from 'react';
import {useNavigate,useParams} from 'react-router-dom';
import Thread from "./Threads";

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
        
        <div className="container" >
            {loading ? (
                <div>A moment please...</div>
            ) : (
            <div>
            <Thread/>
         
            <ListGroup>
                {postsList.map((element) => 
                    <ListGroup.Item className="d-flex justify-content-between align-items-start" action href={"posts/"+element.id} key={element.id} >
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