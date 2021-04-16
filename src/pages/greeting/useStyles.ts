import {makeStyles} from "@material-ui/core";


const useStyles = makeStyles({
  root: {},
  signInContainer: {
    position: 'absolute',
    width: '30%',
    height: 'auto',
    minHeight: '50%',
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

export default useStyles;