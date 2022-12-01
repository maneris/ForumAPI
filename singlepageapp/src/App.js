import './App.css';
import Login from "./pages/Login";
import Register from "./pages/Register";
import Topics from "./pages/Topics";
import Threads from "./pages/Threads";
import Posts from "./pages/Threads";
import TopicsList from "./pages/TopicsList";
import ThreadsList from "./pages/ThreadsList";
import PostsList from "./pages/ThreadsList";
import { Routes, Route  } from 'react-router-dom';


function App() {
  return (
    <main >
      <Routes>
        <Route path='/topics' element={<TopicsList/>} />
        <Route path='/topics/:topicsId' element={<Topics/>} />
        <Route path='/topics/:topicsId/threads' element={<ThreadsList/>} />
        <Route path='/topics/:topicsId/threads/:threadsId' element={<Threads/>} />
        <Route path='/topics/:topicsId/threads/:threadsId/posts' element={<PostsList/>}/>
        <Route path='/topics/:topicsId/threads/:threadsId/posts/:postsId' element={<Posts/>}/>
        <Route path='/login' element={<Login/>} />
        <Route path='/register' element={<Register/>} />
        <Route path='*' element={<Login/> }/>
      </Routes>
    </main>
  );
}
export default App;