import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

export default function FooterContent(){
    return(
        <Container>
            <Row>
                <Col className='offset-md-3 col-md-3'>Atliko Mantas Kvederys</Col>
                <Col className='offset-md-3 col-md-3' ><a href="https://github.com/maneris/ForumAPI">GitHub</a></Col>
            </Row>
        </Container>
    )
}