import {makeStyles} from "@material-ui/core";
import {paleGrey, main} from "../colorConstants";

export default makeStyles({
  purpColor: {
    color: main,
  },
  link: {
    '&:hover': {
      cursor: 'pointer',
      fontWeight: 'bold',
    },
  },
});
