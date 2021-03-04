import {makeStyles} from "@material-ui/core";


const useStyles = makeStyles({
  container: {
    padding: '3% 5%',
  },
  tabs: {
    '& div': {
      background: '#5926EB',
      color: '#fff',
      fontFamily: 'Poppins, sans-serif',
      borderRadius: '5px',
      padding: '10px',
      width: 'fit-content',
      marginBottom: '-5px',
      '&:hover': {
        cursor: 'pointer',
        background: '#fff',
        color: '#5926EB',
      }
    },
  },
  inputContainer: {
    display: 'flex',
    '& > div': {
      flexGrow: 1,
    },
    '& input': {
      background: '#fff',
      borderRadius: '5px',
      boxShadow: '4px 0px 15px rgba(0, 0, 0, 0.1)',
    },
    '& .MuiOutlinedInput-root': {
      '&:hover fieldset': {
        borderColor: '#5926EB',
      },
      '&.Mui-focused fieldset': {
        borderColor: '#5926EB',
      },
    },
  },
  button: {
    width: 'auto',
    marginLeft: '-10px',
    fontWeight: 600,
    fontFamily: 'Poppins, sans-serif',
    fontSize: '14px',
  },
});

export default useStyles;