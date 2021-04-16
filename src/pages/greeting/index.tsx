import React, {useState} from 'react';
import SignIn from "../SignIn";
import {Rounded} from "../../components/button";
import {Box} from "@material-ui/core";
import useStyles from "./useStyles";
import Register from '../../components/forms/register';

export enum Tabs {
  signin,
  register,
  forgot,
}

const Index = () => {
  const [tab, setTab] = useState<Tabs>(Tabs.signin);
  const styles = useStyles();

  return (
    <>
      <Box className={styles.root}>
        <Box className={styles.signInContainer}>
          {tab === Tabs.signin &&
            <SignIn
              renderRegister={(styles: string) => <Rounded
                style={styles}
                onClick={() => setTab(Tabs.register)}
                >Register</Rounded>}
            />}
          {tab === Tabs.register && <Register
            toSignIn={() => setTab(Tabs.signin)}
          />}
        </Box>
      </Box>
    </>
  )
}

export default Index;
