import React, {useState} from 'react';
import SignIn from "./SignIn";
import Button from "../components/button";
import {Box} from "@material-ui/core";
import useStyles from "./useStyles";

export enum Tabs {
  signin,
  register,
  forgot,
}

export default () => {
  const [tab, setTab] = useState<Tabs>(Tabs.signin);
  const styles = useStyles();

  return (
    <>
      <Box className={styles.root}>
        <Box className={styles.signInContainer}>
          {tab === Tabs.signin && <SignIn
            renderRegister={(styles: string) => <Button
              style={styles}
              onClick={() => setTab(Tabs.register)}
            >Register</Button>}
          />}
        </Box>
      </Box>
    </>
  )
}