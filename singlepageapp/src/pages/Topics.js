import '../functionalComponents/CustomStyles.css';
import React, {useState, useEffect} from 'react';
import {useNavigate,useParams} from 'react-router-dom';
import TopicEdit from './Modals/TopicEdit';
import ThreadCreate from './Modals/ThreadCreate';
import Spinner from 'react-bootstrap/Spinner';
import LogoImage from '../functionalComponents/ContentImg2.jpg'
function Topics () {
    const [topic,setTopic] = useState(null);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();
    const params=useParams();
    useEffect(() => {
        fetch(process.env.REACT_APP_API+'topics/'+params.topicsId,{
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
            setTopic(data);
            setLoading(false);
            console.log(data);
        });
    }, [loading])

    async function Delete(){
        await fetch(process.env.REACT_APP_API+'topics/'+params.topicsId,{
            method:'delete',
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
                }else if(response.status==403){
                    alert("Insufficient privilages.");
                }
                else{
                    alert("Error:"+response.status+"\nMessage:"+response.statusText)
                }
            }else{
                navigate('/topics');
            }          
        })
    }
    function Reload(){
        setLoading(true);
    }

    return(
        <div className='container col-md-10 offset-md-1 mt-5 mb-5 p-3 ' style={{ backgroundImage: `url(${LogoImage})`, backgroundSize:`cover`, backgroundRepeat:'no-repeat', backgroundPosition:'center', height:'100%'}}>
            {loading ? (
                <div style={{color:'rgb(255,255,255)'}}>
                <Spinner animation="border" role="status" >
                    <span className="visually-hidden">Loading...</span></Spinner>
                A moment please... 
            </div>
            ) : (
                <div >
                    <p className='title'>{topic.title}</p>
                    <p className='description'>{topic.description}</p>
                    <div className='container-fluid d-flex justify-content-end'>
                    <div className='btn-group' >
                        <TopicEdit Title={topic.title} Description={topic.description} reload={()=>Reload()}/>
                        <button type="button" className='btn btn-secondary' onClick={()=>Delete()} >Delete</button>
                        <ThreadCreate/>
                    </div>
                    </div>  
                </div>
            )}
        </div>
    )
}
export default Topics;