import {makeStyles} from "@material-ui/core";



export default makeStyles({
  root: {

  },
  DiSvgContainer: {
    width: '50%',
    '& img': { width: '100%' },
  },
  signInContainer: {
    position: 'absolute',
    width: '30%',
    height: '50%',
    minHeight: '350px',
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
  inputs: {
    display: 'flex',
    flexDirection: 'column',
    gap: '20px',
  },
  button: {
    borderRadius: '30px',
    marginBottom: '10px',
    '&:hover': {
      color: '#5926EB',
      background: '#fff',
    },
  },

  fullWidth: { width: '100%' },
  marginAuto: { margin: 'auto' },
  whiteBack: {
    background: '#fff',
    color: '#5926EB',
    border: '1px solid #5926EB',
    '&:hover': {
      background: '#5926EB',
      color: 'white',
    }
  },
});