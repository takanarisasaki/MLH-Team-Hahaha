//
//  ViewController.swift
//  mlh-ios
//
//  Created by Chen He on 2018-12-01.
//  Copyright © 2018 Chen He. All rights reserved.
//

import UIKit

class ViewController: UIViewController {

    @IBOutlet var username: UITextField!
    @IBOutlet var password: UITextField!
    @IBAction func test() {
        var data = ["username":username.text!,"password":password.text!]
        HTTP.post(addr: "users/login", data: data, f: testt,fail: failed)
    }
    
    func testt(data:[String:Any]) -> Void {
        for entry in data {
            let v = entry.value as? String
            if v != nil{
                print("\(entry.key) : \(entry.value)")
            }
        }
        global.userdata["username"] = data["username"] as! String
        global.userdata["password"] = data["password"] as! String
        global.userdata["id"] = data["id"] as! String
        DispatchQueue.main.async {
            self.performSegue(withIdentifier: "LOGINSUCCESS", sender: nil)
        }
        
    }
    
    func failed()->Void{
        print("failed")
    }
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
    }


}

