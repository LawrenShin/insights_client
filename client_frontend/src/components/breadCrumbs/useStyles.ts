import {makeStyles} from "@material-ui/core";
import {paleGrey, purp} from "../colorConstants";

export default makeStyles({
  purpColor: {
    color: purp,
  },
  paleFont: {
    fontSize: '.9em',
    color: paleGrey,
  },
  link: {
    '&:hover': {
      cursor: 'pointer',
      fontWeight: 'bold',
    },
  },
});
