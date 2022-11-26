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
          <Route path='/topics' element={<TopicsList/>} >
            <Route path=':topicsId' element={<Topics/>} >
              <Route path='threads' element={<ThreadsList/>} >
                <Route path=':threadsId' element={<Threads/>} >
                  <Route path='posts' element={<PostsList/>}>
                    <Route path=':postsId' element={<Posts/>}/>
                  </Route>
                </Route>
              </Route>
            </Route>
          </Route>
          <Route path='/login' element={<Login/>} />
          <Route path='/register' element={<Register/>} />
      </Routes>
    </main>
  );
}
export default App;