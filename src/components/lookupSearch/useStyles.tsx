import {makeStyles} from "@material-ui/core";
import {main} from "../colorConstants";

const useStyles = makeStyles({
  container: {
    padding: '3% 5%',
    maxHeight: '200px',
  },
  tabs: {
    display: 'flex',
    '& div': {
      fontFamily: 'Poppins, sans-serif',
      borderRadius: '5px',
      padding: '10px',
      width: 'fit-content',
      marginBottom: '-5px',
      '&:hover': {
        cursor: 'pointer',
        background: '#fff',
        color: main,
      }
    },
  },
  selected: {
    background: '#fff',
    color: main,
  },
  unselected: {
    background: main,
    color: '#fff',
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
        borderColor: main,
      },
      '&.Mui-focused fieldset': {
        borderColor: main,
        borderWidth: '1px',
      },
    },
  },
  button: {
    minWidth: '170px',
    width: 'auto',
    marginLeft: '-10px',
    fontWeight: 600,
    fontFamily: 'Poppins, sans-serif',
    fontSize: '14px',
    '&:disabled': {
      background: '#BCB2D9',
    }
  },

  lookupResults: {
    height: 'fit-content',
    background: '#fff',
    position: 'relative',
    zIndex: 9000,
    boxShadow: '0px 2px 20px rgba(0, 0, 0, 0.05)',
    paddingBottom: '20px',
  },
  lookupResultsList: {
    marginBottom: '10px',
  },
  listElement: {
    fontFamily: 'Poppins',
    height: 'fit-content',
    '&:hover': {
      color: main,
      cursor: 'pointer',
      background: 'rgba(188, 178, 217, 0.3)'
    }
  },
  bold: {fontWeight: 'bold'},
  showMore: {
    fontFamily: 'open sans',
    fontWeight: 400,
    color: main,
    background: '#fff',
    border: `1px solid ${main}`,
    borderRadius: '50px',
    margin: '0px 15px',
    height: '35px',
    padding: '0 25px',
    '&:hover': {
      cursor: 'pointer',
      color: '#fff',
      background: main
    },
  },
  result: {
    fontSize: '.9em',
  },
});

export default useStyles;