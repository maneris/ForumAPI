import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';

function MessageModal(){

    return(
        <Modal.Dialog>
            <Modal.Header closeButton>
                <Modal.Title>Modal title</Modal.Title>
            </Modal.Header>

            <Modal.Body>
                <p>Modal body text goes here.</p>
            </Modal.Body>

            <Modal.Footer>
                <Button variant="secondary">Close</Button>
            </Modal.Footer>
        </Modal.Dialog>)
}
export default MessageModal;