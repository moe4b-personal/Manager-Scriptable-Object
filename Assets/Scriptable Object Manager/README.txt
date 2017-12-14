Made By Moe Baker : https://twitter.com/Moe_T4

Known Issues:
-The Init method of the manager will be called after Awake & OnEnable methods of a monobehaviour
but the Configure method is called before anything
so if you Monobehaviour needs to access something from the manager make sure it's setup in the Configure method
or get it in the Monobehaviours Start method where the manager's Init method would've been called