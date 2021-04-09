import {makeStyles} from "@material-ui/core";
import {paleGrey} from "../colorConstants";

export default makeStyles({
  root: {
  },
  titleFont: {
    fontFamily: 'Poppins, sans-serif',
    fontWeight: 'bold',
    fontSize: '1em',
  },
  paintContainer: {
    padding: '10px',
    background: 'white',
    boxShadow: '0px 2px 20px rgba(0, 0, 0, 0.05)',
  },
  paleFont: {
    fontSize: '.9em',
    color: paleGrey,
  },
  separator: {
    width: '1px',
    background: 'rgba(8, 0, 55, .2)',
  },
  ratingStrength: {
    gap: '50px',
    marginTop: '10px'
  }
});
