import '../functionalComponents/CustomStyles.css';
import React, {useState, useEffect} from 'react';
import {useNavigate,useParams} from 'react-router-dom';
import PostCreate from './Modals/PostCreate';

function Threads () {
    const [thread,setThread] = useState(null);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();
    const params=useParams();
    useEffect(() => {
        fetch(process.env.REACT_APP_API+'topics/'+params.topicsId+"/threads/"+params.threadsId,{
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
                }
                else{
                    alert("Error:"+response.status+"\nMessage:"+response.statusText)
                }
            }
            return response.json();           
        })
        .then(data=>{
            setThread(data);
            setLoading(false);
            console.log(data);
        });
    }, [])
    return(
        <div className='container col-md-10 offset-md-1 mt-5 mb-5 p-3 border'>
        {loading ? (
            <div>A moment please...</div>
        ) : (
            <div >
                <p className='title'>{thread.title}</p>
                <p className='description'>{thread.description}</p>
                <div className='container-fluid d-flex justify-content-end'>
                    <div className='btn-group' >
                        
                        <button type="button" className='btn btn-secondary' onClick={()=>console.log("hi")} >Edit</button>
                        <button type="button" className='btn btn-secondary' onClick={()=>console.log("hi")} >Delete</button>
                        <PostCreate />
                    </div>
                </div>  
            </div>
        )}
    </div>
    )
}
export default Threads;