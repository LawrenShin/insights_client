import {makeStyles} from "@material-ui/core";



export default makeStyles({
  root: {},
  signInContainer: {
    position: 'absolute',
    width: '30%',
    height: '45%',
    minHeight: '300px',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    background: '#FFFFFF',
    boxShadow: '0px 4px 25px rgba(89, 38, 235, 0.1)',
    borderRadius: '6px',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    padding: '30px',
    gap: '20px',
  },
});