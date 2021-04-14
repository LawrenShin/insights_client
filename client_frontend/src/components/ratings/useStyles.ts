import {makeStyles} from "@material-ui/core";
import {paleFont} from "../colorConstants";

export default makeStyles({
  ...paleFont,
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
  separator: {
    width: '1px',
    background: 'rgba(8, 0, 55, .2)',
  },
  ratingStrength: {
    gap: '50px',
    marginTop: '10px'
  }
});
