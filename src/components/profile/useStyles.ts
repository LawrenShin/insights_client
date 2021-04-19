import {makeStyles} from "@material-ui/core";
import {purp} from "../colorConstants";

const useStyles = makeStyles({
  root: {
    display: 'flex',
    alignItems : 'center',
    gap: '20px',
    '& h6:hover': {cursor : 'default'},
  },
  usernameContainer: {
    display: 'flex',
    lineHeight: '2.3em',
    gap: '10px',
  },
  arrow: {
    fontSize: '1.1em',
    '& :hover': {
      cursor: 'pointer',
      color: purp,
    }
  },
  logout: { '& button': {margin: 0} },
  logoutClose: {
    // width: 0,
    transform: 'translateX(0)',
    transition: '500ms',
    overflow: 'hidden',
  },
  logoutOpen: {
    // width: 'auto',
    transform: 'translateX(150px)',
    transition: '500ms',
  }
});

export default useStyles;
